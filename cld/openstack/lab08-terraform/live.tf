provider "openstack" {
    user_name   = "admin"
    tenant_name = "demo"
    tenant_id   = "faac34f01fb2464295bcea501b18b741"
    password    = "nomoresecret"
    auth_url    = "http://192.168.17.131/identity"
}

variable "defaults" {
    description = "OpenStack Variables for Tenant"
    type = "map"
    default = { 
       image_name     = "cirros-0.3.5-x86_64-disk"
       az_name        = "nova"
       region         = "RegionOne"
       tenant_name    = "demo"
       flavor_name    = "m.fiap"
       security_group = "default"
       network_name   = "private"
    }
}

resource "openstack_compute_instance_v2" "web" {
  name = "web"
  image_name = "${var.defaults["image_name"]}"
  flavor_name = "${var.defaults["flavor_name"]}"
  availability_zone = "${var.defaults["az_name"]}"
  security_groups = ["${var.defaults["security_group"]}"]
  network {
    name = "${var.defaults["network_name"]}"
  }
  lifecycle {
    create_before_destroy = true
  }
}

resource "openstack_networking_floatingip_v2" "ip-publica" {
  pool = "public"
}

resource "openstack_compute_floatingip_associate_v2" "asoc-ip-publica" {
  floating_ip = "${openstack_networking_floatingip_v2.ip-publica.address}"
  instance_id = "${openstack_compute_instance_v2.web.id}"
}
