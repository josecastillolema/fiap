provider "openstack" {
    user_name   = ""
    tenant_name = ""
    tenant_id   = ""
    password    = ""
    auth_url    = ""
}

variable "defaults" {
    description = "OpenStack Variables for Tenant"
    type = "map"
    default  {
        image_name     = ""
        az_name        = "nova"
        region         = ""
        tenant_name    = ""
        flavor_name    = ""
        security_group = ""
        network_name   = ""
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
