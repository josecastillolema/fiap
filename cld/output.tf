output "ip" {
  value = "${openstack_networking_floatingip_v2.ip-publica.address}"
}
